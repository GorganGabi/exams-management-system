import { Injectable } from '@angular/core';
import {Grade} from '../models/grade';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  grade: Grade;
  constructor() { }
}
