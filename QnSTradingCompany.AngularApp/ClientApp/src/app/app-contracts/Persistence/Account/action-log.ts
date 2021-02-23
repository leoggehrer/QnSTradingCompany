//@QnSGeneratedCode
import { Identity } from '@app-contracts/persistence/account/identity';
/** CustomImportBegin **/
/** CustomImportEnd **/
export interface ActionLog
{
    id: number;
    identityId: number;
    time: Date;
    subject: string;
    action: string;
    info: string;
    identity: Identity;
/** CustomCodeBegin **/
/** CustomCodeEnd **/
}
