import { Byte } from "@angular/compiler/src/util";
import { User } from "./User";

export class Article {
    articleId: number=0;
    title: string='';
    content: string='';
    image: string='';
    articleStatusID: number=0;
    reviewerId: number=0;
    createdBy:number=0;
   
    articleComments:ArticleComments[]=[];
    createdOn:Date=new Date();
    getlike:articleLikes[]=[];
}

export class ArticleComments{
    articleCommentId:number=0;
    comment: string='';
    datetime:any='';
    user: User[]=[];
    createdBy:number=0;
    articleId:Article[]=[];
    createdOn:string='';
    updatedBy: number=0;
    updatedOn:string='';
    article: string='';

}

export class articleLikes{
    
          likeId:number=0;
          articleId:Article[]=[];
          user: User[]=[];
          article: string='';
}