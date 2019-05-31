import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { CoursesComponent } from './components/course-management/courses/courses.component'
import { ExamsComponent } from './components/exam-management/exams/exams.component';
import { CourseDetailComponent } from './components/course-management/course-detail/course-detail.component';
import { LoginComponent } from './components/login/login.component';
import { AddExamComponent } from './components/exam-management/add-exam/add-exam.component';
import { ExamDetailsComponent } from './components/exam-management/exam-details/exam-details.component';
import { AddCourseComponent } from './components/course-management/add-course/add-course.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CoursesComponent,
    CourseDetailComponent,
    ExamsComponent,
    LoginComponent,
    AddExamComponent,
    ExamDetailsComponent,
    AddCourseComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'courses', component: CoursesComponent },
      { path: 'courses/create', component: AddCourseComponent },
      { path: 'courses/:id', component: CourseDetailComponent },
      { path: 'exams', component: ExamsComponent },
      { path: 'exams/create', component: AddExamComponent },
      { path: 'exams/:id', component: ExamDetailsComponent },
      { path: 'login', component: LoginComponent}
    ])
  ],
  exports: [RouterModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
