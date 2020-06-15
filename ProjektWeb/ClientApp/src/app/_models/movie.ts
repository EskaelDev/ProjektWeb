import {Tag} from '../_models/tag';

export class Movie {
  id?: number;
  title: string;
  description: string;
  imagePath: string;
  tags: Tag[];
  score : number;
}
