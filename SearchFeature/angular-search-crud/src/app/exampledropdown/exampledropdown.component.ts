import { Component, OnInit } from '@angular/core';
import { UserService } from '../service/user.service';

@Component({
  selector: 'app-exampledropdown',
  templateUrl: './exampledropdown.component.html',
  styleUrls: ['./exampledropdown.component.css']
})
export class ExampledropdownComponent implements OnInit {
  LocationDropDown: any;

  constructor(private service:UserService) { }

  ngOnInit(): void {
    this.service.getDepList().subscribe((data:any)=>{
      this.LocationDropDown=data;
    }
    
    
    )
  }

}
