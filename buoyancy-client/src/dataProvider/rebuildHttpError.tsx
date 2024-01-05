/* eslint-disable @typescript-eslint/no-unsafe-argument */
/* eslint-disable @typescript-eslint/no-unsafe-member-access */
import { HttpError } from "react-admin";

const buildErrorsFromErrorBody = (errorsBody: {
  [key: string]: string[];
}): { [key: string]: string } => {
  const errors: { [key: string]: string } = {};

  Object.keys(errorsBody).forEach((key) => {
    const errorKey = key[0].toLocaleLowerCase() + key.substring(1);
    errors[errorKey] = errorsBody[key].join(", ");
  });

  return errors;
};

// Rebuild http error into the format which React Admin expects in order to show the errors in line in the form.
const rebuildHttpError = (error: HttpError): Error => {
  if (error.status === 400 && error?.body?.errors) {
    return new HttpError(
      "One or more validation errors occurred, please check the form.",
      400,
      {
        errors: buildErrorsFromErrorBody(error.body.errors),
      },
    );
  }

  return error;
};

export default rebuildHttpError;
