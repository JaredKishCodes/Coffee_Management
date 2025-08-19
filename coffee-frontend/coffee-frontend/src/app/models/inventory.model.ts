import { Category } from "./coffee.model";

export interface CoffeeInventoryDto {
  id: number;
  name: string;
  stock: number;
  isAvailable: boolean;
  category: Category[]
}
