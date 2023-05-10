export interface Project{
    id: string,
    name: string,
    start_date?: Date,
    end_date?: Date,
    deadline?: Date,
    completed: boolean,
    customerId: string
}