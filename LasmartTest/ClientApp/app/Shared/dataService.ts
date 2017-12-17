import { Http, Response } from "@angular/http"
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import 'rxjs/add/operator/map';
import { Headers } from '@angular/http';
import { Equipment } from "./equipment";
import { User } from "./user";

@Injectable()
export class DataService {
    constructor(private http: Http) {

    }
    private token: string = "";
    private tokenExpiration: Date;

    public equipments: Equipment[] = [];

    public user: User = new User();

    public loadEquipments(): Observable<Equipment[]> {
        return this.http.get("/api/equipment")
            .map((result: Response) => this.equipments = result.json());
    }
    public get loginReqired(): boolean {
        return this.token.length == 0 || this.tokenExpiration > new Date();
    }
    public login(creds) {
        return this.http.post("/account/CreateToken", creds)
            .map(response => {
                let tokenInfo = response.json();
                this.token = tokenInfo.token;
                this.tokenExpiration = tokenInfo.expiration;
                this.user.firstName = tokenInfo.firstName;
                this.user.lastName = tokenInfo.lastName;
                return true;
            })
    }

}