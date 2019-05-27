import { Component, OnInit, Input } from '@angular/core';
import { UploadEvent, FileSystemFileEntry } from 'ngx-file-drop';
import { FileSystemItem } from '@models/file-system-item.model';


@Component({
  selector: 'app-file-uploader',
  templateUrl: './file-uploader.component.html',
  styleUrls: ['./file-uploader.component.scss']
})
export class FileUploaderComponent {
  public files: FileSystemItem[] = [];

  @Input() fileTypes = 'DOC, XLS, PDF, JPG, or PNG';
  @Input() extensions: string[] = ['doc', 'xls', 'pdf', 'jpg', 'png'];
  @Input() uploadHeader = 'TO UPLOAD DOCUMENTS, DRAG FILES HERE OR';

  fileSizeLimit = 1048576 * 25; // 25 MB
  fileSizeLimitReadable = '25 MB';

  public dropped(event: UploadEvent) {
    const files = event.files;
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
    const uploadedFiles = event.target.files;
    for (const file of uploadedFiles) {
      if (this.validateFile(file)) {
        this.addFile(file);
      }
    }

    input.value = '';
  }

  validateFile(file: File): boolean {
    const validExt = this.extensions.filter(ex => file.name.toLowerCase().endsWith(ex)).length > 0;
    if (!validExt) {
      alert('File type not supported.');
      return false;
    }

    if (file && file.name && file.name.length > 128) {
      alert('File name must be 128 characters or less.');
      return false;
    }

    if (file && file.size && file.size > this.fileSizeLimit) {
      alert(`The specified file exceeds the maximum file size of ${this.fileSizeLimitReadable}.`);
      return false;
    }

    return true;
  }

  addFile(file: File) {
    this.files.push({ id: this.files.length, name: file.name, size: Math.trunc(file.size / 1024), file: file });
  }

  public fileOver(event) {
    // console.log(event);
  }

  public fileLeave(event) {
    // console.log(event);
  }

  removeFile(file: FileSystemItem) {
    this.files = this.files.filter(f => f.id !== file.id);
  }

  browseFiles(browserMultiple, browserSingle) {
    browserMultiple.click();
  }
}

