//@QnSGeneratedCode
import { ConditionType } from '@app-contracts/modules/common/condition-type';
import { Product } from '@app-contracts/persistence/masterdata/product';
import { Customer } from '@app-contracts/persistence/masterdata/customer';
/** CustomImportBegin **/
/** CustomImportEnd **/
export interface Condition
{
    id: number;
    productId: number;
    customerId: number;
    conditionType: ConditionType;
    quantity: number;
    value: number;
    note: string;
    product: Product;
    customer: Customer;
/** CustomCodeBegin **/
/** CustomCodeEnd **/
}
