export interface OrderDto {
  id: number;
  customerName: string;
  totalPrice: number;
  orderDate: string; // ISO date string from DateTime
  orderStatus: OrderStatus;
  orderItems: OrderItemDto[];
}

export interface OrderItemDto {
  id: number;
  coffeeItemId: number;
  coffeeName: string;
  quantity: number;
  unitPrice: number;
  total: number; // calculated in backend
}
export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T;
}

export enum OrderStatus {
  Pending = 0,
  Processing = 1,
  Completed = 2,
  Cancelled = 3
}