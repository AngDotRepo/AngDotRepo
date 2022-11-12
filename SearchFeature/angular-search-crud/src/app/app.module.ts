import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms'; 
import { AppComponent } from './app.component';
import { SearchFilterPipe } from './search-filter.pipe';

import { MyCustomFilterPipe } from './my-custom.filter';

@NgModule({
  declarations: [
    AppComponent,
    SearchFilterPipe,
    MyCustomFilterPipe
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule
    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
