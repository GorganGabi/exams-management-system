import {Professor} from './professor';

export class Course {
  id: string;
  title: string;
  professors: Professor[];
  exams: object;
  universityYear: string;
  studentYear: number;
  semester: number;
  description: string;
  url: string;
}
