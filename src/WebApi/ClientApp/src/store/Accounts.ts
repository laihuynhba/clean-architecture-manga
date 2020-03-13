import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface AccountsState {
    customer: Customer;
}

export interface Transaction {
    transactionId: string;
    amount: number;
    description: string;
    transactionDate: Date;
}

export interface Account {
    accountId: string;
    currentBalance: number;
    transactions: Transaction[];
}

export interface Customer {
    customerId: string;
    ssn: string;
    name: string;
    accounts: Account[];
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestAccountsAction {
    type: 'REQUEST_ACCOUNTS';
}

interface ReceiveAccountsAction {
    type: 'RECEIVE_ACCOUNTS';
    customer: Customer;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestAccountsAction | ReceiveAccountsAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestAccounts: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        fetch(`api/v1/Customers`)
            .then(response => response.json() as Promise<Customer>)
            .then(data => {
                dispatch({ type: 'RECEIVE_ACCOUNTS', customer: data });
            });
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: AccountsState = { customer: { customerId: "", name: "", ssn: "", accounts: [] } };

export const reducer: Reducer<AccountsState> = (state: AccountsState | undefined, incomingAction: Action): AccountsState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'RECEIVE_ACCOUNTS':
            return {
                customer: action.customer
            };
            break;
    }

    return state;
};
