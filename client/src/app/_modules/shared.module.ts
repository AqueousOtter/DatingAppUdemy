import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';

// Module to cut down on the imports for app-modules

@NgModule({
  declarations: [],
  imports: [
    CommonModule, // has to be in every module
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    })
  ], 
  exports: [
    BsDropdownModule,
    ToastrModule
  ]
})
export class SharedModule { }
