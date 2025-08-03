import Link from "next/link";

export default function DashBoardPage() {
    return (
        <div className="container">
            <main className="content">

            </main>

            <aside className="sidebar">
                <h3>Панель управления</h3>
                <ul>
                    <li><Link href={"/admin/dashboard/create_new_page"}>Добавить страницу</Link></li>
                    <li><Link href={"/admin/dashboard/get_all_pages"}>Все страницы</Link></li>
                 
                </ul>
            </aside>
        </div>



    );
}