//@QnSGeneratedCode
import { Condition } from '@app-contracts/persistence/app/condition';
import { Order } from '@app-contracts/persistence/app/order';
/** CustomImportBegin **/
/** CustomImportEnd **/
export interface Customer
{
    id: number;
    number: string;
    name: string;
    conditions: Condition[];
    orders: Order[];
/** CustomCodeBegin **/
/** CustomCodeEnd **/
}
