export interface UserInvestmentDto {
  userInvestmentId: number;
  symbol: string;
  quantity: number;
  buyPrice: number;
  currentPrice: number;
  purchaseDate: string;  // ISO 8601 from API
  pl: number;
  investmentTypeName: string;
}