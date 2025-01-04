import { person } from './person';

export class personInfo {
    Person: person;
    Status: boolean;
    ErrorType: ErrorTypes;
}



export enum ErrorTypes {
    errorEmail,
    errorPassword,
    errorDisable,
    personAlreadyExist
}