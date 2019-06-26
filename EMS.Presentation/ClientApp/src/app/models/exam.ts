import {Course} from './course';

export class Exam {
  course: Course;
  date: Date;
  id: string;
  room: string;
  type: string;
  imagePath: string;
  professorIds: string[];
}
