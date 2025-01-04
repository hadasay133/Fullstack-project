import { person } from './person';
import { request } from './request';

export class Offer {
     id: number;
     id_person: number;
     resourse: string;
     destination: string;

     resourse_city: string;
     destination_city: string;
     remarks: string;


     seats: number;
     date_time: Date;//כולל שעה?TODO
     price: number;
     active: boolean;

     requests: request[];
     driver: person;
     numSeats: number;
     is_emailed: boolean;
}

