export type PaginatedData<T> = {
  data: T[]
  pagination: { totalRecords: number; page: number; perPage: number }
}
