//@QnSGeneratedCode
import { Product } from '@app-contracts/persistence/masterdata/product';
import { Customer } from '@app-contracts/persistence/masterdata/customer';
/** CustomImportBegin **/
/** CustomImportEnd **/
export interface Order
{
    id: number;
    productId: number;
    customerId: number;
    createdOn: Date;
    count: number;
    priceNet: number;
    discount: number;
    product: Product;
    customer: Customer;
/** CustomCodeBegin **/
/** CustomCodeEnd **/
}
