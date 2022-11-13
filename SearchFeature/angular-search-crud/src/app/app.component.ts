import { Component } from '@angular/core';
import { IOrder } from './model/IOrder';
import { IOrderDropDown } from './model/IOrderDropDown';
import { IPost } from './model/IPost';
import { UserService } from './service/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  posts$:  IPost[]= [];
  orders$:  IOrder[]= [];
  searchOrders$:  IOrder[]= [];
  dropDown$:  IOrderDropDown[]= [];
  title = 'angular-search-crud';
  searchText: string | undefined;
  constructor(public userService : UserService){
  }
  
  ngOnInit() {
    
    this.userService.getOrders().subscribe(res => { 
      this.orders$ = res;
      });

      // this.userService.getLocationNames().subscribe(res => { 
      //   this.searchOrders$ = res;
      //   });

        this.userService.getLocationNamesDropDown().subscribe(res => { 
          this.dropDown$ = res;
          });

      // this.userService.getOrders().subscribe(res => { 
      //   this.posts$ = res;
      //   console.log(this.orders$);
      //   console.log(this.posts$);
      //   });

      let value = (<HTMLSelectElement>document.getElementById('dropdown-content')).value;

      console.log(value);
      
  }
}