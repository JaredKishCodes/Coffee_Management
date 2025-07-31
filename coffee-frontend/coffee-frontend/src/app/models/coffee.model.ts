export interface CoffeeCategory {
  id: number;
  name: string;
  description: string;
  imageUrl: string;
}

export interface CoffeeCategoryApiResponse {
  success: boolean;
  message: string;
  data: CoffeeCategory[]; // 🔥 this is the array *ngFor needs
}
