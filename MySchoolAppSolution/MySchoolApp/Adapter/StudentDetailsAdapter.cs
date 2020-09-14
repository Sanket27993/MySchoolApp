using System;
using MySchool;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using System.Linq;

namespace MySchoolApp
{
    class StudentDetailsAdapter : RecyclerView.Adapter
    {
        public event EventHandler<AdapterClickEventArgs> EditItemClick;
        public event EventHandler<AdapterClickEventArgs> DeleteItemClick;

        public List<StudentDetailsModel> _studentList { get; set; }
        public List<UserOAuthenticationModel> _userList { get; set; }
        public StudentDetailsAdapter(List<StudentDetailsModel> studentList,List<UserOAuthenticationModel> userList)
        {
            this._studentList = studentList;
            _userList = userList;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.item_student_details;
            itemView = LayoutInflater.From(parent.Context).
                   Inflate(id, parent, false);

            var vh = new StudentDetailsAdapterViewHolder(itemView, OnClickEdit, OnClickDelete);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = _studentList[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as StudentDetailsAdapterViewHolder;
            holder.textViewName.Text = item.Name;
            holder.textViewAddress.Text = item.Address;
            holder.textViewContactNo.Text = item.ContactNo;
          
            if (item.IsAudited)
            {
                holder.textViewAudited.Text = "Audited";
            }
            else
            {
                holder.textViewAudited.Text = "Unaudited";
            }

            var userData = _userList.Where(rc => rc.StudentID == item.StudentID).FirstOrDefault();
            if(userData!=null)
            {
                holder.textViewUserName.Text = userData.UserName;
            }
        }

        public override int ItemCount => _studentList.Count;

        void OnClickEdit(AdapterClickEventArgs args) => EditItemClick?.Invoke(this, args);
        void OnClickDelete(AdapterClickEventArgs args) => DeleteItemClick?.Invoke(this, args);
    }

    public class StudentDetailsAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public TextView textViewName;
        public TextView textViewAddress;
        public TextView textViewContactNo;
        public ImageView imageViewEdit;
        public ImageView imageViewDelete;
        public TextView textViewAudited;
        public TextView textViewUserName;
        public StudentDetailsAdapterViewHolder(View itemView, Action<AdapterClickEventArgs> editclickListener,
                            Action<AdapterClickEventArgs> deleteClickListener) : base(itemView)
        {
            textViewName = itemView.FindViewById<TextView>(Resource.Id.textViewName);
            textViewAddress = itemView.FindViewById<TextView>(Resource.Id.textViewAddress);
            textViewContactNo = itemView.FindViewById<TextView>(Resource.Id.textViewContactNo);
            textViewAudited = itemView.FindViewById<TextView>(Resource.Id.textViewAudited);
            textViewUserName = itemView.FindViewById<TextView>(Resource.Id.textViewUserName);

            imageViewEdit = itemView.FindViewById<ImageView>(Resource.Id.imageViewEdit);
            imageViewDelete = itemView.FindViewById<ImageView>(Resource.Id.imageViewDelete);

            imageViewEdit.Click += (sender, e) => editclickListener(new AdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            imageViewDelete.Click += (sender, e) => deleteClickListener(new AdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class AdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}