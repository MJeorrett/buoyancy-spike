/* eslint-disable @typescript-eslint/no-explicit-any */
/* eslint-disable @typescript-eslint/no-unsafe-member-access */
/* eslint-disable @typescript-eslint/no-unsafe-call */
/* eslint-disable @typescript-eslint/no-unsafe-assignment */
/* eslint-disable @typescript-eslint/no-unused-vars */

import {
  CreateParams,
  CreateResult,
  DataProvider,
  DeleteManyParams,
  DeleteManyResult,
  DeleteParams,
  DeleteResult,
  GetListParams,
  GetListResult,
  GetManyReferenceParams,
  GetManyReferenceResult,
  GetManyResult,
  GetOneParams,
  GetOneResult,
  HttpError,
  Identifier,
  RaRecord,
  UpdateManyParams,
  UpdateManyResult,
  UpdateParams,
  UpdateResult,
  fetchUtils,
} from "react-admin";

import rebuildHttpError from "./rebuildHttpError";

const apiBaseUrl = "https://localhost:7808/api";
const httpClient = fetchUtils.fetchJson;

const dataProvider: DataProvider = {
  getList: async <RecordType extends RaRecord<Identifier> = any>(
    resource: string,
    params: GetListParams
  ): Promise<GetListResult<RecordType>> => {
    const { page, perPage } = params.pagination;
    const query = {
      pageIndex: page - 1,
      pageSize: perPage,
      orderBy: params.sort?.field,
      sortOrder: params.sort?.order.toLowerCase(),
      searchTerm: params.filter?.searchTerm,
    };

    const filters: { [key: string]: string } = {};

    for (const key in params.filter) {
      if (params.filter[key] !== undefined) {
        filters[key] = params.filter[key].toString();
      }
    }

    const filterParams = fetchUtils.queryParameters(filters);
    const queryParams = filterParams
      ? `${fetchUtils.queryParameters(query)}&${filterParams}`
      : fetchUtils.queryParameters(query);

    const url = `${apiBaseUrl}/${resource}?${queryParams}`;

    try {
      const { json } = await httpClient(url);

      return {
        data: json.content.items,
        total: json.content.totalCount,
      };
    } catch (error: any) {
      if (error instanceof HttpError) {
        throw rebuildHttpError(error);
      } else {
        throw error;
      }
    }
  },
  getOne: async <RecordType extends RaRecord<Identifier> = any>(
    resource: string,
    params: GetOneParams<RecordType>
  ): Promise<GetOneResult<RecordType>> => {
    try {
      const url =
        resource === "me"
          ? `${apiBaseUrl}/me`
          : `${apiBaseUrl}/${resource}/${params.id}`;

      const { json } = await httpClient(url);

      return {
        data: json.content,
      };
    } catch (error: any) {
      if (error instanceof HttpError) {
        throw rebuildHttpError(error);
      } else {
        throw error;
      }
    }
  },

  getMany: async <RecordType extends RaRecord<Identifier> = any>(
    resource: string
  ): Promise<GetManyResult<RecordType>> => {
    const query = {
      pageIndex: 1,
      pageSize: 1000,
    };
    try {
      const queryParams = fetchUtils.queryParameters(query);
      const url = `${apiBaseUrl}/${resource}?${queryParams}`;

      const { json } = await httpClient(url);

      return {
        data: json.content.items,
      };
    } catch (error: any) {
      if (error instanceof HttpError) {
        throw rebuildHttpError(error);
      } else {
        throw error;
      }
    }
  },

  getManyReference: function <RecordType extends RaRecord<Identifier> = any>(
    _resource: string,
    _params: GetManyReferenceParams
  ): Promise<GetManyReferenceResult<RecordType>> {
    throw new Error("DataProvider function not implemented.");
  },

  update: async <RecordType extends RaRecord<Identifier> = any>(
    resource: string,
    params: UpdateParams<any>
  ): Promise<UpdateResult<RecordType>> => {
    try {
      const url = `${apiBaseUrl}/${resource}/${params.id}`;

      await httpClient(url, {
        method: "PUT",
        body: JSON.stringify(params.data),
      });

      return { data: params.data as any };
    } catch (error: any) {
      if (error instanceof HttpError) {
        throw rebuildHttpError(error);
      } else {
        throw error;
      }
    }
  },

  updateMany: function <RecordType extends RaRecord<Identifier> = any>(
    _resource: string,
    _params: UpdateManyParams<any>
  ): Promise<UpdateManyResult<RecordType>> {
    throw new Error("DataProvider function not implemented.");
  },

  create: async <
    RecordType extends Omit<RaRecord<Identifier>, "id"> = any,
    ResultRecordType extends RaRecord<Identifier> = RecordType & {
      id: Identifier;
    }
  >(
    resource: string,
    params: CreateParams<any>
  ): Promise<CreateResult<ResultRecordType>> => {
    try {
      const path = params.meta?.path ?? resource;
      const { json, status } = await httpClient(`${apiBaseUrl}/${path}`, {
        method: "POST",
        body: JSON.stringify(params.data),
      });

      if (
        status === 207 &&
        (resource === "base-products-bulk-upload" ||
          "product-variants-bulk-upload")
      ) {
        throw new HttpError(JSON.stringify(json.content), 207);
      }

      return { data: { ...params.data, id: json.id } as any };
    } catch (error: any) {
      if (
        error instanceof HttpError &&
        resource !== "base-products-bulk-upload" &&
        resource !== "product-variants-bulk-upload"
      ) {
        throw rebuildHttpError(error);
      } else {
        throw error;
      }
    }
  },

  delete: async <RecordType extends RaRecord<Identifier> = any>(
    resource: string,
    params: DeleteParams<RecordType>
  ): Promise<DeleteResult<RecordType>> => {
    const url = `${apiBaseUrl}/${resource}/${params.id}`;

    await httpClient(url, { method: "DELETE" });

    return { data: { ...params.previousData, id: params.id } as any };
  },

  deleteMany: function <RecordType extends RaRecord<Identifier> = any>(
    _resource: string,
    _params: DeleteManyParams<RecordType>
  ): Promise<DeleteManyResult<RecordType>> {
    throw new Error("DataProvider function not implemented.");
  },
};

export default dataProvider;
