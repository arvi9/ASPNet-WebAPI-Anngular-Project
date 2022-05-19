import { Injectable } from '@angular/core';
import { data } from 'jquery';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  static GetData(key:string):string|null {
   const itemStr = localStorage.getItem(key)
   if (!itemStr) {
     return null
   }
   const item = JSON.parse(itemStr)
   const now = new Date()
   if (now.getTime() > item.expiry) {
     localStorage.removeItem(key)
     return null
   }
   return item.value
 }

  static SetDateWithExpiry(key:string,value:string, expiryInMinutes:number) {
   const now = new Date()
   expiryInMinutes =expiryInMinutes*60;

   const item = {
     value: value,

     expiry: now.getTime() + expiryInMinutes,
   }
   localStorage.setItem(key, JSON.stringify(item))
 }

 static IsAdmin():boolean{

  return this.GetData("Admin")?.includes("true")? true: false;

 }
 static IsReviewer():boolean{
  return this.GetData("Reviewer")?.includes("true")?true:false;
}

static Logout(){
  localStorage.clear();
}
}
