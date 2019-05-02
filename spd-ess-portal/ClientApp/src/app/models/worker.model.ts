import { DynamicsContact } from './dynamics-contact.model';

export interface Worker {
    id: string;
    isldbworker: boolean;
    firstname: string;
    middlename: string;
    lastname: string;
    dateofbirth: string;
    gender: string;
    birthplace: string;
    driverslicencenumber: string;
    bcidcardnumber: string;
    phonenumber: string;
    email: string;
    selfdisclosure: string;
    triggerphs: string;
    bCIDCardNumber: string;
    contactId: string;
    paymentReceived: boolean;
    paymentReceivedDate: Date;
    workerId: string;
    contact: DynamicsContact;
    modifiedOn: Date;
    status: string;
}
