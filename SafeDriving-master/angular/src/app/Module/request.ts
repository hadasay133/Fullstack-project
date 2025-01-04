import { Offer } from './offer';
import { travel } from './Travels';
import { person } from './person';

export class request {
    id: number;
    id_person: number;
    resourse: string;
    destination: string;
    resourse_city: string;
    destination_city: string;
    seats: number;
    date_time: Date;
    active: boolean;
    ignore_offers: string;
    remarks: string;
    is_emailed: boolean;
    email_offers: string;



    offer: Offer;
    travel: travel;
    driver: person;
}

