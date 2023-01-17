export interface Post {
    id: number;
    posterId: number;
    username: string;
    title: string;
    date: string;
    desc?: string;
    pictureURL?: string;
    tag?: Array<string>;
}