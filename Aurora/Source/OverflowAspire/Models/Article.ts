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
    authorName:string='';
    status:string="";
    comments:ArticleComments[]=[];
    date:Date=new Date();
    getlike:articleLikes[]=[];
    likes:number=0;
    reviewer:string=""
}

export class ArticleComments{
    CommentId: number=0;
    message: string="";
    name:string="";


}

export class articleLikes{

          likeId:number=0;
          articleId:Article[]=[];
          user: User[]=[];
          article: string='';
}
