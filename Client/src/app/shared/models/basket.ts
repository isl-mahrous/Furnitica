
export interface IBasket {
    id: number;
    basketItems: IBasketItem[];
    userId: string;
}

export interface IBasketItem {
    id: number;
    price: number;
    quantity: number;
    productId: number;
    basketId: number;
}

export interface IBasketTotals {
    shipping: number,
    subtotal: number,
    total: number
}







