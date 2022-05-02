import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { FileSystemFileEntry, NgxFileDropEntry } from 'ngx-file-drop';

import * as FileUploadsActions from '../../app-state/actions/file-uploads.action';
import { AppState } from '../../app-state/models/app-state';

import { FileSystemItem } from '../../models/file-system-item.model';

@Component({
  selector: 'app-file-uploader[id]',
  templateUrl: './file-uploader.component.html',
  styleUrls: ['./file-uploader.component.scss']
})
export class FileUploaderComponent implements OnInit, OnDestroy {
  unsubscribe: Subject<void> = new Subject();
  files: FileSystemItem[] = [];

  @Input() id: string;
  @Input() fileTypes = 'PDF, DOC, DOCX, XLS, XLSX, BMP, JPG, JPEG, PNG, TIF, or TIFF';
  @Input() extensions: string[] = ['pdf', 'doc', 'docx', 'xls', 'xlsx', 'bmp', 'jpg', 'jpeg', 'png', 'tif', 'tiff'];
  @Input() uploadHeader = 'TO UPLOAD DOCUMENTS, DRAG FILES HERE OR';
  @Input() maxFileCount = 10;

  fileSizeLimit = 1048576 * 25; // 25 MB
  fileSizeLimitReadable = '25 MB';

  validationErrors: string[] = [];

  constructor(private store: Store<AppState>) { }

  ngOnInit() {
    // subscribe to files from store
    this.store.select(state => state.fileUploadsState.fileUploads)
      .pipe(
        takeUntil(this.unsubscribe),
      ).subscribe(fileUploads => {
        const fileUploadSet = fileUploads.find(f => f.id === this.id);
        this.files = fileUploadSet ? fileUploadSet.files : [];
      });
  }

  ngOnDestroy() {
    this.unsubscribe.next();
    this.unsubscribe.complete();
  }

  public dropped(files: NgxFileDropEntry[]) {
    this.validationErrors = [];

    for (const droppedFile of files) {
      if (droppedFile.fileEntry.isFile) {
        const fileEntry = droppedFile.fileEntry as FileSystemFileEntry;
        fileEntry.file((file: File) => {
          if (this.validateFile(file)) {
            this.addFile(file);
          }
        });
      }
    }
  }

  onBrowserFileSelect(event: any, input: any) {
    this.validationErrors = [];

    const uploadedFiles = event.target.files;
    for (const file of uploadedFiles) {
      if (this.validateFile(file)) {
        this.addFile(file);
      }
    }

    input.value = '';
  }

  validateFile(file: File): boolean {
    const validExt = this.extensions.filter(ex => file.name.toLowerCase().endsWith('.' + ex)).length > 0;
    if (!validExt) {
      this.validationErrors.push(`File type not supported. <em>[${file.name}]</em>`);
      return false;
    }

    if (file && file.name && file.name.length > 128) {
      this.validationErrors.push(`File name must be 128 characters or less. <em>[${file.name}]</em>`);
      return false;
    }

    if (file && file.size && file.size > this.fileSizeLimit) {
      const limit = this.fileSizeLimitReadable;
      this.validationErrors.push(`The specified file exceeds the maximum file size of ${limit}. <em>[${file.name}]</em>`);
      return false;
    }

    if (this.maxFileCount && this.files.length >= this.maxFileCount) {
      this.validationErrors.push(`File limit has been reached. The specified file has not been added. <em>[${file.name}]</em>`);
      return false;
    }

    return true;
  }

  addFile(file: File) {
    const fileSystemEntry = { id: this.files.length, name: file.name, size: Math.trunc(file.size / 1024), file: file };
    this.store.dispatch(new FileUploadsActions.SetFileUploadsAction({ id: this.id, files: [...this.files, fileSystemEntry ] }));
  }

  removeFile(file: FileSystemItem) {
    this.store.dispatch(new FileUploadsActions.SetFileUploadsAction({ id: this.id, files: this.files.filter(f => f.id !== file.id) }));
  }

  browseFiles(browserMultiple: HTMLInputElement) {
    browserMultiple.click();
  }
}
