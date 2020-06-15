import {Tag} from './tag';

export class Movie {
  id?: number;
  title: string;
  description: string;
  imagePath: string;
  tags: Tag[];
}
