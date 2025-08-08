export interface AddCartItemRequest{
  coffeeItemId: number;
  quantity: number;
}

export interface AddCartDto {
  customerName: string;
  cartItems: AddCartItemRequest[];
}



export interface CartItemDto {
  id: number;
  coffeeItemImg : string;
  coffeeItemId: number;
  quantity: number;
  unitPrice: number;
  total: number;
  // add other fields if needed
}

export type OrderStatus = 'Pending' | 'Processing' | 'Completed' | 'Cancelled'; 

export interface CartDto {
  id: number;
  customerName: string;
  totalPrice: number;
  orderDate: string; // or Date if you parse it
  orderStatus: OrderStatus;
  cartItems: CartItemDto[];
}

export interface CartResponse {
  success: boolean;
  message: string;
  data: CartDto;
}

