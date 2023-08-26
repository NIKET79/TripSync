import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StoreService {

  private store = new Map<string, any>;
  constructor() { }

  get(key: string) {
    return this.store.get(key);
  }

  set(key: string, value: any) {
    this.store.set(key, value);
  }
}
