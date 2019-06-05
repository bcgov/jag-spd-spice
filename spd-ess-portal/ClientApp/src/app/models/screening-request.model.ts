import { FileSystemItem } from './file-system-item.model';

export class ScreeningRequest {
  clientMinistry: string;
  programArea: string;
  screeningType: string;
  reason: string;
  candidateFirstName: string;
  candidateMiddleName: string;
  candidateLastName: string;
  candidateDateOfBirth: Date;
  candidateEmail: string;
  candidatePosition: string;
  contactFirstName: string;
  contactLastName: string;
  contactEmail: string;
  files: FileSystemItem[] = [];
}
