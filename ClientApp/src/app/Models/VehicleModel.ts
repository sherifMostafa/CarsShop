export class VehicleModel {
   id:number;
   name : string;
   modelId : number;
   isRegistered : boolean;
   contact : contact;
   features : Array<number>;

}

class contact {
    name:string;
    email:string;
    phone:string;
}

