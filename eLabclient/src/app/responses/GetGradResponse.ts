import { Guid } from 'guid-typescript';

export class GetGradResponse{

    constructor(
        public id:Guid,
        public name:string,
        public PTT:number
    ){}
}
