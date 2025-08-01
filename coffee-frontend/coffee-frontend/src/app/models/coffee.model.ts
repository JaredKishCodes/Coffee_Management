export interface CoffeeCategory {
  id: number;
  name: string;
  description: string;
  imageUrl: string;
}

export interface Coffee {
  id: number;
  name: string;
  description: string;
  price: number;
  size: number;
  stock: number;
  isAvailable: boolean;
  imageUrl: string;
}

export interface CoffeeCategoryApiResponse {
      success: boolean;
      message: string;
      data: CoffeeCategory[]; // 🔥 this is the array *ngFor needs
}

export interface CoffeesApiResponse{
  success: boolean;
  message: string;
  data: Coffee[]; // 🔥 this is the array *ngFor needs
}
