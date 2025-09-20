import Link from "next/link";
import { ListGroup } from "react-bootstrap";
export default function AdminHeader() {
    return (
        < aside >
            <h3>Панель управления</h3>
            <ListGroup>
                <Link href={"/admin/dashboard/create_new_page"}>Добавить страницу</Link>
                <Link href={"/admin/dashboard/get_all_pages"}>Все страницы</Link>
            </ListGroup>
        </aside >
    )
}