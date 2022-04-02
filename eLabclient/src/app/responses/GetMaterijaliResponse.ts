import { Guid } from 'guid-typescript';

export class GetMaterijaliResponse{

    constructor(
        public id:Guid,
        public name:string, 
        public quantity:number, 
        public description: string
    ){}
}