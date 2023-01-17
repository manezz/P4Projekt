export interface Post {
    id: number;
    posterId: number;
    title: string;
    date: string;
    desc?: string;
    pictureURL?: string;
    tag?: Array<string>;
}