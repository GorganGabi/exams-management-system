import {Injectable} from '@angular/core';
import {Grade} from '../models/grade';

@Injectable({
  providedIn: 'root'
}) // todo: delete this service
export class DataService {
  grade: Grade;

  constructor() {
  }
}
