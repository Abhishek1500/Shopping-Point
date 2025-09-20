import { Product } from "./Product";

export interface CategorySummary{
    id: number,
    categoryName: string,
    products:Product[]
}