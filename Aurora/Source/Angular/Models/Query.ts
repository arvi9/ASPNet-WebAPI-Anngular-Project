export class Query {
    queryId: number=0;
    title!: string;
    content!: string;
    code: string="";
    isSolved: boolean=false;
    queryComments:QueryComment[]=[];
    constructor(){}
    
}

export class QueryComment{
        queryCommentId: number=0;
        comment: string="";
        createdOn: string="";
        queryId: number=0;
        code: string="";
    
}