import React from "react";
import Styles from "../Styles";
import { Form, Field } from "react-final-form";
import TextInput from "../components/TextInput";
import NumberInput from "../components/NumberInput";
import {connect} from "react-redux";

interface Values {
    accountId: string;
    amount: number;
}

const onSubmit = async (values: Values) => {
    const formData  = new FormData();

    formData.append('accountId', values.accountId);
    formData.append('amount', values.amount.toString());

    await fetch('api/v1/Accounts/Withdraw', {
        method: 'POST',
        body: formData
    });
};

const Withdraw: React.FC = () => (
    <Styles>
        <h1>Withdraw</h1>
        <Form
            onSubmit={onSubmit}
            initialValues={{ accountId: '', amount: 0}}
            render={({ handleSubmit, form, submitting, pristine, values }) => (
                <form onSubmit={handleSubmit}>
                    <div>
                        <label>Account</label>
                        <Field<string>
                            name="AccountId"
                            component={TextInput}
                            placeholder="Account"
                        />
                    </div>
                    <div>
                        <label>Initial Amount</label>
                        <Field<number>
                            name="amount"
                            component={NumberInput}
                            placeholder="amount"
                        />
                    </div>
                    <div className="buttons">
                        <button type="submit" disabled={submitting || pristine}>
                            Submit
                        </button>
                    </div>
                </form>
            )}
        />
    </Styles>
);

export default connect()(Withdraw);
