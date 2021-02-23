//@QnSGeneratedCode
import { Condition } from '@app-contracts/persistence/app/condition';
import { Order } from '@app-contracts/persistence/app/order';
/** CustomImportBegin **/
/** CustomImportEnd **/
export interface Product
{
    id: number;
    number: string;
    name: string;
    price: number;
    conditions: Condition[];
    orders: Order[];
/** CustomCodeBegin **/
/** CustomCodeEnd **/
}
