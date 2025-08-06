export interface CoffeeCategory {
  id: number;
  name: string;
  description: string;
  imageUrl: string;
}

export interface Category {
  id: number;
  name: string;
}

export interface Coffee {
  id: number;
  name: string;
  description: string;
  price: number;
  size: 'Small' | 'Medium' | 'Large';
  stock: number;
  isAvailable: boolean;
  imageUrl: string;
  categoryId: number;
  category: Category;
}

export interface CoffeeResponse{
  success: boolean;
  message: string;
  data: Coffee;
}


export interface CoffeeCategoryApiResponse {
      success: boolean;
      message: string;
      data: CoffeeCategory[]; 
}

export interface CoffeesApiResponse{
  success: boolean;
  message: string;
  data: Coffee[]; 
}
