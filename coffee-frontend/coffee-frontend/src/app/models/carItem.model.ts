import { Coffee } from "./coffee.model";

export interface CartItem {
  id: number;
  cartId: number;
  cart: Cart;
  coffeeItemId: number;
  coffeeItem: Coffee;
  quantity: number;
  unitPrice: number;
  total: number;
}

export interface Cart {
  id: number;
  customerName: string;
  totalPrice: number;
  createdAt?: string; // or `Date` if you parse it
  cartItems: CartItem[];
}

export interface AddCartItemDto{
    coffeeItemId: number;
    quantity: number;
    cartId: number;
}

export interface CartItemDto {
  success: boolean;
  message: string;
  data: CartItem;
}