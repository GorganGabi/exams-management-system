import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {Observable, Subject} from 'rxjs';
import {Student} from '../../../models/student';
import {StudentService} from '../../../services/student.service';
import {debounceTime, distinctUntilChanged, switchMap} from 'rxjs/operators';

@Component({
  selector: 'app-student-search',
  templateUrl: './student-search.component.html',
  styleUrls: ['./student-search.component.css']
})
export class StudentSearchComponent implements OnInit {
  students$: Observable<Student[]>;
  private searchTerms = new Subject<string>();
  name = '';
  @Output() OnStudentName: EventEmitter<any> = new EventEmitter<any>();

  constructor(private studentService: StudentService) {
  }

  search(term: string): void {
    this.searchTerms.next(term);
  }

  ngOnInit() {
    this.students$ = this.searchTerms.pipe(
      // wait 300ms after each keystroke before considering the term
      debounceTime(300),

      // ignore new term if same as previous term
      distinctUntilChanged(),

      // switch to new search observable each time the term changes
      switchMap((term: string) => this.studentService.getStudentsByName(term)),
    );
  }

  public setInputValue(name: string): void {
    this.name = name;
    this.OnStudentName.emit(name);
  }
}
