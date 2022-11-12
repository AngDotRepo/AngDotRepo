import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { map } from 'rxjs/operators';
import { Observable, observable } from 'rxjs';


@Injectable({
   providedIn : 'root'    
})

export class UserService {


constructor(private http:HttpClient) {}

protected posturl = 'http://localhost:8000';

protected orderurl = 'https://localhost:44371/api/Videogame';

getPosts(): Observable<any> {
 return this
          .http
           .get(`${this.posturl}/posts`)
           .pipe(
               map(res=> res)
           );
        }

getOrders(): Observable<any> {
            return this
                     .http
                      .get(`${this.orderurl}/GetAllVideogames`)
                      .pipe(
                          map(res=> res)
                      );
                   }

getLocationNames(): Observable<any> {
            return this
                             .http
                              .get(`${this.orderurl}/GetLocationNamesByChar`)
                              .pipe(
                                  map(res=> res)
                              );
                           }

getLocationNamesDropDown(): Observable<any> {
            return this
                    .http
                    .get(`${this.orderurl}/GetLocationNamesDropDown`)
                    .pipe(
                        map(res=> res)
                        );
                    }
    }