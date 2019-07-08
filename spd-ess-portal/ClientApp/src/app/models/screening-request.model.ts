import { Candidate } from './candidate.model';
import { Contact } from './contact.model';
import { FileSystemItem } from './file-system-item.model';

export class ScreeningRequest {
  clientMinistry: string;
  programArea: string;
  screeningType: string;
  reason: string;
  otherReason: string;
  candidate: Candidate;
  contact: Contact;
  files: FileSystemItem[] = [];
}
