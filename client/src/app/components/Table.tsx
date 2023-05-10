import { Project } from "../services/projects/types";

export default function Table({ projects }: { projects: Project[] }) {
    return (
        <table className="table-fixed w-full">
            <thead className="bg-gray-200">
                <tr>
                    <th className="border px-4 py-2">#</th>
                    <th className="border px-4 py-2">Project Name</th>
                    <th className="border px-4 py-2">Start Date</th>
                    <th className="border px-4 py-2">Deadline</th>
                </tr>
            </thead>
            <tbody>
                {
                    projects.map((p) => {
                        return(
                        <tr>
                            <td className="border px-4 py-2">{p.id}</td>
                            <td className="border px-4 py-2">{p.name}</td>
                            <td className="border px-4 py-2">{p.start_date?.toDateString()}</td>
                            <td className="border px-4 py-2">{p.deadline?.toDateString()}</td>
                        </tr>
                        )
                    })
                }
            </tbody>
        </table>
    );
}
