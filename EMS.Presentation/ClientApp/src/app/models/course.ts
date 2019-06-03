import {Professor} from './professor';

export class Course {
  id: string;
  title: string;
  professor: Professor;
  exams: object;
  universityYear: string;
  studentYear: number;
  semester: number;
}
