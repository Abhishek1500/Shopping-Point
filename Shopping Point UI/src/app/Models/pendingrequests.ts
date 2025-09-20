export interface PendinRequest{
    id: number,
    userId: number,
    userEmail: string,
    userName: string,
    productId: number,
    productName: string,
    productCompany: string,
    imageurl: string,
    price: number,
    categoryName: string,
    status: string,
    count:number,
    lastUpdated:Date
}